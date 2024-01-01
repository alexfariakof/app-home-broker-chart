import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public magazineLuizaHistoryPrices?: MagazineLuizaHistoryPrice[];

  constructor(http: HttpClient) {
    const period = {
      StartDate: "2023-01-01",
      EndDate: "2024-01-01"
    }

    http.get<MagazineLuizaHistoryPrice[]>(`ChartHomeBroker?StartDate=${period.StartDate}&EndDate=${period.EndDate}`).subscribe(result => {
      this.magazineLuizaHistoryPrices = result as MagazineLuizaHistoryPrice[];
    },
    error =>
     console.error(error));
  }

  title = 'Chart Home Broker';
}

interface MagazineLuizaHistoryPrice {
  date: string;
  open: number;
  high: number;
  low: number;
  close: number;
  adjClose: number;
  volume: number;
}
