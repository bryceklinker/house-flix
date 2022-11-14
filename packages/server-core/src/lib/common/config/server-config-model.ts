import { z } from 'zod';
import { OMDBConfigSchema } from '../omdb';

export const ServerConfigSchema = z.object({
  omdb: OMDBConfigSchema,
  database: z.object({
    host: z.string(),
    port: z.number(),
    username: z.string(),
    password: z.string(),
  }),
});

export type ServerConfig = z.infer<typeof ServerConfigSchema>;
