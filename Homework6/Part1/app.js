const kopekBakimUtility = require('./kopekBakimUtility');
const kopek = require('./kopek');

console.log("kopek adi :", kopek.ismi);
console.log("kopek boyu :", kopek.boyu, "cm");
kopekBakimUtility.kopekTemizle();
console.log("kopek ilgi saati :", kopek.kilo * kopekBakimUtility.kopekBakimSaati);
