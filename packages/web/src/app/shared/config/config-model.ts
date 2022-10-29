import { z } from 'zod';

export const ConfigModelSchema = z.object({
  api: z.object({
    url: z.string().url(),
  }),
});

export type ConfigModel = z.infer<typeof ConfigModelSchema>;
