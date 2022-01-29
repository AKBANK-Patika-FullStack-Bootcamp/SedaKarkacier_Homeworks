const array = [2,3,4];

let girlsPowerToplami = s => {
    s = s/2 + 3;
    return s;
}

function girlsPower (array) {
    let result;
    result = array.map(x => girlsPowerToplami(x));
    return result;
}

console.log("verilen array:", array)
console.log("cikti:", girlsPower(array));