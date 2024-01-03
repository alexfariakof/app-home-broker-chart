import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Dayjs } from "dayjs";
import { MagazineLuizaHistoryPrice } from "../../interfaces";

@Injectable({
  providedIn: 'root'
})

export class ChartService {
  constructor(public http: HttpClient) {
  }

  async getSMA(): Promise<any> {
    const result = await this.http.get<any>(`ChartHomeBroker/GetSMA`).toPromise();
    return result.values;
  }

  async getEMA(periodDays: number): Promise<any> {
    const result = await this.http.get<any>(`ChartHomeBroker/GetEMA/${periodDays}`).toPromise();
    return result.values;
  }

  async get(startDate:Dayjs, endDate: Dayjs): Promise<MagazineLuizaHistoryPrice[]> {
    const result:MagazineLuizaHistoryPrice[] = await this.http.get<any>(`ChartHomeBroker?StartDate=${startDate}&EndDate=${endDate}`).toPromise();
    return result;
  }
}
