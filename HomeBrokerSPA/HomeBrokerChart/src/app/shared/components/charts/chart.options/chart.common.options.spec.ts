import { ChartCommonOptions, ChartOptions } from "./chart.common.options";
import { seriesData } from "./mock.chart.data";

describe('ChartCommonOptions', () => {
  let chartOptions: ChartOptions | any;

  beforeEach(() => {
    chartOptions = {
      chart: {
        height: 260,
        type: "line",
        zoom: {
          enabled: false
        }
      },
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: "straight"
      },
      title: {
        text: "MAGAZINE LUIZA S.A.",
        align: "left"
      },
      grid: {
        row: {
          colors: ["#f3f3f3", "transparent"],
          opacity: 0.5
        }
      },
      xaxis: {
        labels: {
          show: false,
        },
      },
    };
  });

  it('should initialize chart options with the provided height', () => {
    ChartCommonOptions.initializeChartOptions(chartOptions, 500);

    expect(chartOptions.chart.height).toEqual(500);
  });

  it('should initialize chart data with given data and labelXAxis', () => {
    const data = [10, 20, 30];
    const labelXAxis = ['Jan', 'Feb', 'Mar'];

    ChartCommonOptions.initializeChartData(chartOptions, data, labelXAxis);

    expect(chartOptions.series).toEqual(data);
    expect(chartOptions.xaxis.categories).toEqual(labelXAxis);
  });

  it('should format data by rounding to two decimal places', () => {
    const input = [10.123, 20.456, 30.789];
    const expectedOutput = [10.12, 20.46, 30.79];

    const result = ChartCommonOptions.formatData(input);

    expect(result).toEqual(expectedOutput);
  });
});
