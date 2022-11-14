import { OMDBClient } from './omdb-client';
import {
  createServerCoreTestingModule,
  OMDBModelFactory,
  OMDBTestingApi,
  TESTING_SERVER_CONFIG,
} from '../../../testing';
import { RestRequest } from 'msw';
import { OMDBPlotType, OMDBType } from './models';

describe('OMDBClient', () => {
  let client: OMDBClient;

  beforeEach(async () => {
    const { resolve } = await createServerCoreTestingModule();
    client = await resolve(OMDBClient);
  });

  test('when searching then returns search results', async () => {
    const expected = OMDBModelFactory.many(
      OMDBModelFactory.createSearchItem,
      45
    );
    OMDBTestingApi.setupSearch(expected);

    const result = await client.search({ term: 'idk' });

    expect(result.Search).toEqual(expected);
    expect(result.Response).toEqual(true);
  });

  test('when searching then adds search params to query', async () => {
    const requests: RestRequest[] = [];
    OMDBTestingApi.setupSearch([], { capture: (req) => requests.push(req) });

    await client.search({
      term: 'lin',
      year: '2020',
      type: OMDBType.movie,
      page: 45,
    });

    const searchParams = requests[0].url.searchParams;
    expect(searchParams.get('apikey')).toEqual(
      TESTING_SERVER_CONFIG.omdb.apiKey
    );
    expect(searchParams.get('s')).toEqual('lin');
    expect(searchParams.get('y')).toEqual('2020');
    expect(searchParams.get('page')).toEqual('45');
    expect(searchParams.get('type')).toEqual('movie');
    expect(searchParams.get('r')).toEqual('json');
  });

  test('when getting by title then returns matching result', async () => {
    const expected = OMDBModelFactory.createItem();
    const requests: RestRequest[] = [];
    OMDBTestingApi.setupGetItem(expected, {
      capture: (req) => requests.push(req),
    });

    const item = await client.getByTitle('lincoln');

    expect(item).toEqual(expected);
    expect(requests[0].url.searchParams.get('t')).toEqual('lincoln');
  });

  test('when getting title with parameters then adds parameters to request', async () => {
    const requests: RestRequest[] = [];
    OMDBTestingApi.setupGetItem(OMDBModelFactory.createItem(), {
      capture: (req) => requests.push(req),
    });

    await client.getByTitle('title', {
      type: OMDBType.episode,
      year: '2012',
      plot: OMDBPlotType.long,
    });

    const searchParams = requests[0].url.searchParams;
    expect(searchParams.get('type')).toEqual(OMDBType.episode);
    expect(searchParams.get('y')).toEqual('2012');
    expect(searchParams.get('plot')).toEqual(OMDBPlotType.long);
  });

  test('when getting by title returns missing result then returns error response', async () => {
    OMDBTestingApi.setupGetItem(OMDBModelFactory.createErrorItem());

    const result = await client.getByTitle('idk');

    expect(result.Response).toEqual(false);
  });
});
