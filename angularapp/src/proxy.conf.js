const PROXY_CONFIG = [
  {
    context: [
      "/ChartHomeBroker",
    ],
    target: "https://localhost:7288",
    secure: false
  }
]

module.exports = PROXY_CONFIG;
