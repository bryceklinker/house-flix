import { Test } from '@nestjs/testing';
import { ServerCoreModule } from '../lib';
import { TypeOrmModule } from '@nestjs/typeorm';
import { CommandBus, EventBus, QueryBus } from '@nestjs/cqrs';
import { Type } from '@nestjs/common';
import { ConfigModule } from '@nestjs/config';
import { ServerConfig } from '../lib/common/config';
import { TESTING_SERVER_CONFIG } from './testing-server-config';

export type CreateServerCoreTestingModuleResult = {
  commandBus: CommandBus;
  queryBus: QueryBus;
  eventBus: EventBus;
  resolve: <T>(type: Type<T>) => Promise<T>;
};

export async function createServerCoreTestingModule(): Promise<CreateServerCoreTestingModuleResult> {
  const module = await Test.createTestingModule({
    imports: [
      ConfigModule.forRoot({
        load: [() => TESTING_SERVER_CONFIG],
      }),
      ServerCoreModule,
      TypeOrmModule.forRoot({
        type: 'better-sqlite3',
        database: ':memory:',
        dropSchema: true,
        synchronize: true,
      }),
    ],
    exports: [ServerCoreModule],
  }).compile();
  await module.init();
  return {
    commandBus: await module.resolve(CommandBus),
    eventBus: await module.resolve(EventBus),
    queryBus: await module.resolve(QueryBus),
    resolve: (type) => module.resolve(type),
  };
}
