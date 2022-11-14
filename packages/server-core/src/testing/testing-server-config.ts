import { ServerConfig } from '../lib';

export const TESTING_SERVER_CONFIG: ServerConfig = {
  omdb: {
    url: 'https://api.omdbtesting.com',
    apiKey: '123456789',
  },
  database: {
    host: '',
    password: '',
    port: 4444,
    username: '',
  },
};
