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
    const result:IMagazineLuizaHistoryPrice[] = await this.http.get<any>(`${ this.routeUrl }/${startDate}/${endDate}`).toPromise();
    return result;
  }

  async getSMA(startDate:Dayjs| string, endDate: Dayjs | string): Promise<any> {
    const result = await this.http.get<any>(`${ this.routeUrl }/GetSMA/${startDate}/${endDate}`).toPromise();
    return result;
  }

  async getEMA(periodDays: number, startDate:Dayjs| string, endDate: Dayjs | string): Promise<any> {
    const result = await this.http.get<any>(`${ this.routeUrl }/GetEMA/${periodDays}/${startDate}/${endDate}`).toPromise();
    return result;
  }

  async getMACD(startDate:Dayjs| string, endDate: Dayjs | string): Promise<any> {
    const result = await this.http.get<any>(`${ this.routeUrl }/GetMACD/${startDate}/${endDate}`).toPromise();
    return result;
  }
}
