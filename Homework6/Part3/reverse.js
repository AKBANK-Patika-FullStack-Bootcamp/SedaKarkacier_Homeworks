//1. Method
function first(str) { 
    return str.split("").reverse().join(""); 
} 
console.log(first('seda'));

//2. Method, for loop method
function second(str) { 
    var str1 = "";
    for (var i = str.length - 1; i >= 0; i--) { 
        str1 += str[i]; 
    } 
    return str1; 
} 
console.log(second('seda'));

//3. Method, recursive method
function third(str) { 
    if (str === "") 
        return "";
    else 
        return third(str.substr(1)) + str.charAt(0); 
} 
console.log(third('seda'));

