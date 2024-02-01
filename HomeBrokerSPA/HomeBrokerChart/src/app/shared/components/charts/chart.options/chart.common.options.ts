import {
  ApexAxisChartSeries,
  ApexChart,
  ApexXAxis,
  ApexDataLabels,
  ApexYAxis,
  ApexFill,
  ApexStroke,
  ApexMarkers,
  ApexGrid,
  ApexTitleSubtitle,
  ApexPlotOptions,
} from "ng-apexcharts";

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
  static readonly DEFAULT_CHART_OPTIONS: ChartOptions | any  = {
    series: [{ name: "X", color: "#000", data: [0, 0, 0, 0, 0] }],
    chart: {
      height: (document.body.clientHeight / 3) - 16,
      type: "line",
      zoom: {
        type: "x",
        enabled: false,
        autoScaleYaxis: false,
      },
    },
    fill: {
      type: "gradient",
      gradient: {
        shadeIntensity: 1,
        inverseColors: false,
        opacityFrom: 0.5,
        opacityTo: 0,
        stops: [1, 2, 4, 8],
      },
    },
    dataLabels: {
      enabled: false,
    },
    stroke: {
      width: [2],
      curve: "straight",
    },
    title: {
      text: "Nenhuma informação encontrada...",
      align: "left",
    },
    grid: {
      row: {
        colors: ["#f3f3f3", "transparent"],
        opacity: 0.5,
      },
    },
    xaxis: {
      labels: {
        show: false,
      },
    },
    yaxis: [
      {
        opposite: true,
      },
    ],
    colors: []
  };

  static initializeChartData(chartOptions: any, data: any[], labelXAxis: any[]): void {
    chartOptions.series = data;
    chartOptions.xaxis.categories = labelXAxis;
  }

  static formatData(data: number[]): number[] {
    return data.map((value) => parseFloat(value.toFixed(2))) || [];
  }
}
