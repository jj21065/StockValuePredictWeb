import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-value-predict-main',
  templateUrl: './value-predict-main.component.html',
  styleUrls: ['./value-predict-main.component.scss'],
})
export class ValuePredictMainComponent implements OnInit {
  stockList = [
    { index: 3552, name: '同致', value: 252 },
    { index: 2330, name: '台積電', value: 589 },
  ];
  constructor() {}

  ngOnInit(): void {
    console.log('run');
  }
}
