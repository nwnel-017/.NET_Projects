using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Assignment;

public record struct PingResult(int ExitCode, string? StdOutput);

public class PingProcess
{
    private ProcessStartInfo StartInfo { get; } = new("ping");

    public PingResult Run(string hostNameOrAddress)
    {
        StartInfo.Arguments = hostNameOrAddress;
        StringBuilder? stringBuilder = null;
        void updateStdOutput(string? line) =>
            (stringBuilder??=new StringBuilder()).AppendLine(line);
        Process process = RunProcessInternal(StartInfo, updateStdOutput, default, default);
        return new PingResult( process.ExitCode, stringBuilder?.ToString());
    }
/*1---> Implement PingProcess' public Task<PingResult> RunTaskAsync(string hostNameOrAddress) ✔
First implement public void RunTaskAsync_Success() test method to test PingProcess.RunTaskAsync() using "localhost".
Do NOT use async/await in this implementation. ✔ */
    public Task<PingResult> RunTaskAsync(string hostNameOrAddress)
    {
        return Task.Run(
            () =>
                Run(hostNameOrAddress)
            );
    }
/*2---> Implement PingProcess' async public Task<PingResult> RunAsync(string hostNameOrAddress) ✔
First implement the public void RunAsync_UsingTaskReturn_Success() test method to test PingProcess.RunAsync() using "localhost" without using async/await. ✔
Also implement the async public Task RunAsync_UsingTpl_Success() test method to test PingProcess.RunAsync() using "localhost" but this time DO using async/await. ✔ */
/*3---> Add support for an optional cancellation token to the PingProcess.RunAsync() signature. ✔ 
 
 * Inside the PingProcess.RunAsync() invoke the token's ThrowIfCancellationRequested() method so an exception is thrown. ✔ 
 * Test that, when cancelled from the test method, the exception thrown is an AggregateException ✔ 
 * with a TaskCanceledException inner exception ✔ using the test methods RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrapping ✔
 * and RunAsync_UsingTplWithCancellation_CatchAggregateExceptionWrappingTaskCanceledException ✔ 
 * respectively.*/
    async public Task<PingResult> RunAsync(
        string hostNameOrAddress, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        Task<PingResult> result = Task.Run(
            () => Run(hostNameOrAddress), cancellationToken);
        await result;
        PingResult pingResult = result.Result;
        return pingResult;
    }
    /*4---> Complete/fix AND test async public Task<PingResult> RunAsync(IEnumerable<string> hostNameOrAddresses, CancellationToken cancellationToken = default) 
     which executes ping for and array of hostNameOrAddresses (which can all be "localhost") in parallel, adding synchronization if needed. ❌✔ 
    NOTE:
    The order of the items in the stdOutput is irrelevent and expected to be intermingled.
    StdOutput must have all the ping output returned (no lines can be missing) even though intermingled. ❌✔ */
    async public Task<PingResult> RunAsync(IEnumerable<string> hostNameOrAddresses, CancellationToken cancellationToken = default)
    {
        StringBuilder stringBuilder = new();
        //This query runs in parallel-> for each string in hostNameOrAddresses, it runs a new task to ping the item
        ParallelQuery<Task<PingResult>>? all = hostNameOrAddresses.AsParallel().Select(async item =>
        {
            //Task<PingResult> task = null!;
            // ...
            Task<PingResult> task = Task.Run(
                () => Run(item), cancellationToken
            );

            await task.WaitAsync(default(CancellationToken)); //waits for the task to finish with using default cancellation token
            //return task.Result.ExitCode;
            return task.Result; //returns the result of the ping
        });

        //await Task.WhenAll(all); //waits for the parallel query to finish pinging each string in hostnameOrAddress array
        //int total = all.Aggregate(0, (total, item) => total + item.Result);

        await Task.WhenAll(all);

        //all.Aggregate(stringBuilder, (a, item) => stringBuilder.Append(item.Result.StdOutput));
        int total = all.Aggregate(0, (total, item) => total + item.Result.ExitCode);
        stringBuilder.Append(all.Aggregate("", (string1, string2) =>
        string1.Trim() + string2.Result.StdOutput));
        return new PingResult(total, stringBuilder?.ToString().Trim());
    }
    //5
    async public Task<PingResult> RunLongRunningAsync(
        string hostNameOrAddress, CancellationToken cancellationToken = default)
    {
        /* Task task = null!;
         await task;
         throw new NotImplementedException();*/
        Task<PingResult> task = Task.Factory.StartNew(
             () => Run(hostNameOrAddress), cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Current);
        await task;
        return task.Result;
    }

    private Process RunProcessInternal(
        ProcessStartInfo startInfo,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        var process = new Process
        {
            StartInfo = UpdateProcessStartInfo(startInfo)
        };
        return RunProcessInternal(process, progressOutput, progressError, token);
    }

    private Process RunProcessInternal(
        Process process,
        Action<string?>? progressOutput,
        Action<string?>? progressError,
        CancellationToken token)
    {
        process.EnableRaisingEvents = true;
        process.OutputDataReceived += OutputHandler;
        process.ErrorDataReceived += ErrorHandler;

        try
        {
            if (!process.Start())
            {
                return process;
            }

            token.Register(obj =>
            {
                if (obj is Process p && !p.HasExited)
                {
                    try
                    {
                        p.Kill();
                    }
                    catch (Win32Exception ex)
                    {
                        throw new InvalidOperationException($"Error cancelling process{Environment.NewLine}{ex}");
                    }
                }
            }, process);


            if (process.StartInfo.RedirectStandardOutput)
            {
                process.BeginOutputReadLine();
            }
            if (process.StartInfo.RedirectStandardError)
            {
                process.BeginErrorReadLine();
            }

            if (process.HasExited)
            {
                return process;
            }
            process.WaitForExit();
        }
        catch (Exception e)
        {
            throw new InvalidOperationException($"Error running '{process.StartInfo.FileName} {process.StartInfo.Arguments}'{Environment.NewLine}{e}");
        }
        finally
        {
            if (process.StartInfo.RedirectStandardError)
            {
                process.CancelErrorRead();
            }
            if (process.StartInfo.RedirectStandardOutput)
            {
                process.CancelOutputRead();
            }
            process.OutputDataReceived -= OutputHandler;
            process.ErrorDataReceived -= ErrorHandler;

            if (!process.HasExited)
            {
                process.Kill();
            }

        }
        return process;

        void OutputHandler(object s, DataReceivedEventArgs e)
        {
            progressOutput?.Invoke(e.Data);
        }

        void ErrorHandler(object s, DataReceivedEventArgs e)
        {
            progressError?.Invoke(e.Data);
        }
    }

    private static ProcessStartInfo UpdateProcessStartInfo(ProcessStartInfo startInfo)
    {
        startInfo.CreateNoWindow = true;
        startInfo.RedirectStandardError = true;
        startInfo.RedirectStandardOutput = true;
        startInfo.UseShellExecute = false;
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        return startInfo;
    }
}
