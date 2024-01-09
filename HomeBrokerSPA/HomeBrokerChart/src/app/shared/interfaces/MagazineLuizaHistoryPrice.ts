import { Dayjs } from "dayjs";
export interface MagazineLuizaHistoryPrice {
  date: Dayjs;
  open: number;
  high: number;
  low: number;
  close: number;
  adjClose: number;
  volume: number;
}
