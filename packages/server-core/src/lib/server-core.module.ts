import { Module } from '@nestjs/common';
import { CqrsModule } from '@nestjs/cqrs';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MovieEntity, PersonEntity, MovieRoleEntity } from './movies';
import { ConfigModule } from '@nestjs/config';
import { OMDBClient } from './common/omdb';

@Module({
  imports: [
    ConfigModule,
    CqrsModule,
    TypeOrmModule.forFeature([MovieEntity, PersonEntity, MovieRoleEntity]),
  ],
  providers: [OMDBClient],
  exports: [TypeOrmModule],
})
export class ServerCoreModule {}
