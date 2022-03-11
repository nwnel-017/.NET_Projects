
function getJoke(){
    let jokeText = document.querySelector(".joke");
    axios
    ({
        method: 'get',
        url: "https://v2.jokeapi.dev/joke/Programming"
    })
    .then(function (response){
        console.log(response);

        let jokeType = response.data.type;
        console.log(jokeType);

        if(jokeType == "single"){
            jokeText.innerText = response.data.joke;
        } else if(jokeType == "twopart") {
            jokeText.innerText = response.data.setup;
            let punchline = document.querySelector(".punchline");
            punchline.style.visibility = 'hidden';
            punchline.innerText = response.data.delivery;

            setTimeout(revealDelivery, 4000);
        }
        else {
            jokeText.innerText = "Please try again in a few seconds";
        }
    })
    .catch(function (error) 
   {
      console.log(console);
      jokeText.innerText = "Something went wrong, please try again in a few moments";
   });
}

function revealDelivery(){
    let punchline = document.querySelector(".punchline");
    punchline.style.visibility = 'visible';
}