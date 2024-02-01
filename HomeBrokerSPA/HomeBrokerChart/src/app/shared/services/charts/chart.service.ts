import { lastValueFrom } from 'rxjs';
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
    const magazineLuizaHistoryPrice$ = this.http.get<any>(`${ this.routeUrl }/${startDate}/${endDate}`);
    const result = await lastValueFrom(magazineLuizaHistoryPrice$) as IMagazineLuizaHistoryPrice[];
    return result;
  }

  async getSMA(startDate:Dayjs| string, endDate: Dayjs | string): Promise<any> {
    const getSMA$ = this.http.get<any>(`${ this.routeUrl }/getsma/${startDate}/${endDate}`);
    const result = await lastValueFrom(getSMA$);
    return result;
  }

  async getEMA(periodDays: number, startDate:Dayjs| string, endDate: Dayjs | string): Promise<any> {
    const getEMA$ = this.http.get<any>(`${ this.routeUrl }/getema/${periodDays}/${startDate}/${endDate}`);
    const result = await lastValueFrom(getEMA$);
    return result;

  }

  async getMACD(startDate:Dayjs| string, endDate: Dayjs | string): Promise<any> {
    const getMACD$ = this.http.get<any>(`${ this.routeUrl }/getmacd/${startDate}/${endDate}`);
    const result = await lastValueFrom(getMACD$);
    return result;
  }
}
