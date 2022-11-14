import { z } from 'zod';

export enum OMDBType {
  movie = 'movie',
  series = 'series',
  episode = 'episode',
}

export enum OMDBResultType {
  json = 'json',
  xml = 'xml',
}

export enum OMDBPlotType {
  short = 'short',
  long = 'long',
}

export const OMDBConfigSchema = z.object({
  url: z.string(),
  apiKey: z.string(),
});

export const OMDBResponseSchema = z.object({
  Response: z.union([z.string(), z.boolean()]).transform((v) => Boolean(v)),
});

export const OMDBErrorResponseSchema = OMDBResponseSchema.extend({
  Error: z.string(),
});

export const OMDBItemResponseSchema = OMDBResponseSchema.extend({
  Title: z.string(),
  Year: z.string(),
  Rated: z.string(),
  Released: z.string(),
  Runtime: z.string(),
  Genre: z.string(),
  Director: z.string(),
  Actors: z.string(),
  Plot: z.string(),
  Language: z.string(),
  Poster: z.string(),
});

export const OMDBSearchItemSchema = z.object({
  Title: z.string(),
  Year: z.string(),
  imdbID: z.string(),
  Type: z.nativeEnum(OMDBType),
  Poster: z.string().url(),
});

export const OMDBSearchResponseSchema = OMDBResponseSchema.extend({
  Search: z.array(OMDBSearchItemSchema),
  totalResults: z.number(),
});

export type OMDBConfig = z.infer<typeof OMDBConfigSchema>;
export type OMDBErrorResponse = z.infer<typeof OMDBErrorResponseSchema>;
export type OMDBItemResponse = z.infer<typeof OMDBItemResponseSchema>;
export type OMDBSearchItem = z.infer<typeof OMDBSearchItemSchema>;
export type OMDBSearchResponse = z.infer<typeof OMDBSearchResponseSchema>;

export type OMDBGetParameters = {
  title?: string;
  id?: string;
  type?: OMDBType;
  year?: string;
  plot?: OMDBPlotType;
};

export type OMDBSearchParameters = {
  term: string;
  type?: OMDBType;
  year?: string;
  page?: number;
};
