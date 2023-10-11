// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const runSum = async (n1, n2) => {
    if (n1 == 0 && n2 !== 0) {
        return n1 + n2;
    }
    else {
        return 1;
    }
}
let a = 10;
let b = 20;
let result=await runSum(a,b)