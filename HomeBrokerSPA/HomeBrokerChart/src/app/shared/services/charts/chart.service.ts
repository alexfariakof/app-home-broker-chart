import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Dayjs } from "dayjs";
import { IMagazineLuizaHistoryPrice } from "../../interfaces";

@Injectable({
  providedIn: 'root'
})

export class ChartService {
  public routeUrl:string = 'ChartHomeBroker';

  constructor(public http: HttpClient) {
  }

  async get(startDate:Dayjs| string, endDate: Dayjs | string): Promise<IMagazineLuizaHistoryPrice[]> {
    const result:IMagazineLuizaHistoryPrice[] = await this.http.get<any>(`${ this.routeUrl }?StartDate=${startDate}&EndDate=${endDate}`).toPromise();
    return result;
  }

  async getSMA(): Promise<any> {
    const result = await this.http.get<any>(`${ this.routeUrl }/GetSMA`).toPromise();
    return result.values;
  }

  async getEMA(periodDays: number): Promise<any> {
    const result = await this.http.get<any>(`${ this.routeUrl }/GetEMA/${periodDays}`).toPromise();
    return result.values;
  }
}
