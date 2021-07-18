'use strict'
const switcher = document.querySelector('.btn');
switcher.addEventListener('click', function () {
    console.log('hello world');
    fetch('https://localhost:44373/api/StockApi/GetProfitDetail', { method: 'GET',headers: new Headers({
        'Content-Type': 'application/json'
    })})
        .then(function (response) {
            return response.json();
        })
        .then(function (myJson) {
            console.log(myJson);
        });
    this.textContent = 'OK!';
});

