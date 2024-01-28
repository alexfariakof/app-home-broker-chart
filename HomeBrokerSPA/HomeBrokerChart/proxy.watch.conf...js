const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:5001` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:5000';

const PROXY_CONFIG = [
  {
    context: [
      "/ChartHomeBroker",
   ],
    target: target,
    secure: false,
    headers: {
      Connection: 'Keep-Alive'
    }
  }
]

module.exports = PROXY_CONFIG;
