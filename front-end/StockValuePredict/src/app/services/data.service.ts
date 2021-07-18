import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { environment } from 'src/environments/environment';

const apiURL = environment.apiURL;

@Injectable({
  providedIn: 'root',
})
export class DataService {
  constructor(private http: HttpClient) {}

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
