var name = "Inigo Montoya";
var person = {
    name: "Inigo Montoya",
    age: function() {
        return new Date().getFullYear() - this.year;
    }, 
    year: 1987,
    weapon: "sword",
    getName: function() {
        return this.name;
    }
};

function showName() {
    console.log(this.name);
}
console.log("Hello World: " + person.getName() + " is " + person.age());

document.getElementById("mybutton").addEventListener("click", function(){
    console.log("Hello World: " + person.getName() + " is " + person.age());
});