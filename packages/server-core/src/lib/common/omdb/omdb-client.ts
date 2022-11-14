import { Injectable } from '@nestjs/common';
import { ConfigService } from '@nestjs/config';
import { Axios } from 'axios';
import {
  OMDBConfig,
  OMDBErrorResponse,
  OMDBErrorResponseSchema,
  OMDBGetParameters,
  OMDBItemResponse,
  OMDBItemResponseSchema,
  OMDBResultType,
  OMDBSearchParameters,
  OMDBSearchResponse,
  OMDBSearchResponseSchema,
} from './models';
import { Lazy } from '../lazy';

@Injectable()
export class OMDBClient {
  private readonly _axios: Lazy<Axios>;

  get axios(): Axios {
    return this._axios.value;
  }

  constructor(private readonly config: ConfigService) {
    this._axios = new Lazy<Axios>(() => this.createClient());
  }

  async search(params: OMDBSearchParameters): Promise<OMDBSearchResponse> {
    let searchParams: Record<string, unknown> = { s: params.term };

    if (params.year) searchParams = { ...searchParams, y: params.year };
    if (params.type) searchParams = { ...searchParams, type: params.type };
    if (params.page) searchParams = { ...searchParams, page: params.page };

    const response = await this.get(searchParams);
    return OMDBSearchResponseSchema.parse(response);
  }

  async getByTitle(
    title: string,
    params?: Omit<OMDBGetParameters, 'title' | 'id'>
  ): Promise<OMDBItemResponse | OMDBErrorResponse> {
    let searchParams: Record<string, unknown> = { t: title };

    if (params?.type) searchParams = { ...searchParams, type: params.type };
    if (params?.year) searchParams = { ...searchParams, y: params.year };
    if (params?.plot) searchParams = { ...searchParams, plot: params.plot };

    const response = (await this.get(searchParams)) as
      | OMDBItemResponse
      | OMDBErrorResponse;
    return response.Response
      ? OMDBItemResponseSchema.parse(response)
      : OMDBErrorResponseSchema.parse(response);
  }

  private async get(params: Record<string, unknown>): Promise<unknown> {
    const response = await this.axios.get<string>('', {
      params,
    });
    return JSON.parse(response.data);
  }

  private createClient() {
    const { apiKey, url } = this.config.get<OMDBConfig>('omdb');
    return new Axios({
      baseURL: url,
      params: {
        apikey: apiKey,
        r: OMDBResultType.json,
      },
    });
  }
}
