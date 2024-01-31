import { ApexAxisChartSeries, ApexChart, ApexXAxis, ApexDataLabels, ApexYAxis, ApexFill, ApexStroke, ApexMarkers, ApexGrid, ApexTitleSubtitle, ApexPlotOptions } from "ng-apexcharts";

export type ChartOptions = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  xaxis: ApexXAxis;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  fill: ApexFill;
  stroke: ApexStroke;
  markers: ApexMarkers;
  grid: ApexGrid;
  title: ApexTitleSubtitle;
  colors: string[];
};

export class ChartCommonOptions {
  static initializeChartOptions(chartOptions: ChartOptions, height: number): void {
    // Lógica comum para inicializar os ChartOptions
    // ...
    chartOptions.chart.height = height;
  }

  static initializeChartData(chartOptions: any, data: any[], labelXAxis: any[]): void {
    chartOptions.series = data;
    chartOptions.xaxis.categories = labelXAxis;
  }

  static formatData(data: number[]): number[] {
    return data.map((value) => parseFloat(value.toFixed(2)));
  }
}
