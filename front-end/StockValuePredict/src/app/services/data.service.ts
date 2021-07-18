import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const url = environment.apiURL;

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {}

  getMonthRevenueInfo(year: number, month: number, stockId: number) {
    const queryURL = url + 'MonthlyRevenue';
    const query = {
      year: year,
      month: month,
      stockId: stockId,
    };
    return this.http.post<MonthRevenue>(queryURL, query);
  }

  // getSOOutputProductivity(startDate, endDate) {
  //   const qURL = `${apiURL}/WorkHourEfficiency/SOOutputProductivity`;
  //   const params = new HttpParams().set('Start', startDate).set('End', endDate);
  //   return this.http.get<SOOutputProductivity[]>(qURL, { params });
  // }

  // getSOWorkHourEfficiency(startDate, endDate) {
  //   const qURL = `${apiURL}/WorkHourEfficiency/SOEfficiency`;
  //   const params = new HttpParams().set('Start', startDate).set('End', endDate);
  //   return this.http.get<SOEfficiency[]>(qURL, { params });
  // }
}
export interface MonthRevenue {}
