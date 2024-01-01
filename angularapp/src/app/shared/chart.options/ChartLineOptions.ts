import { ApexAxisChartSeries, ApexChart, ApexXAxis, ApexDataLabels, ApexYAxis, ApexFill, ApexStroke, ApexMarkers, ApexGrid, ApexTitleSubtitle } from "ng-apexcharts";

export type ChartLineOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  yaxis: ApexYAxis;
  fill: ApexFill;
  stroke: ApexStroke;
  markers: ApexMarkers;
  grid: ApexGrid;
  title: ApexTitleSubtitle;
  colors: string[];
};
