import { ApexAxisChartSeries, ApexChart, ApexXAxis, ApexDataLabels, ApexYAxis, ApexStroke, ApexPlotOptions } from "ng-apexcharts";

export type ChartCandleOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  yaxis: ApexYAxis;
  plotOptions: ApexPlotOptions;
  dataLabels: ApexDataLabels;
  stroke: ApexStroke;
};
