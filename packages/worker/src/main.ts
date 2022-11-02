/**
 * This is not a production server yet!
 * This is only a minimal backend to get started.
 */

import { Logger } from '@nestjs/common';
import { NestFactory } from '@nestjs/core';

import { AppModule } from './app/app.module';
import { MicroserviceOptions, Transport } from '@nestjs/microservices';
import { createHouseFlixLogger } from '@house-flix/server-core';

async function bootstrap() {
  await NestFactory.createMicroservice<MicroserviceOptions>(AppModule, {
    logger: createHouseFlixLogger({ appName: 'worker' }),
    transport: Transport.MQTT,
    options: {
      url: 'mqtt://mqtt:1883',
    },
  });
  Logger.log(`🚀 Application is running on: mqtt://mqtt:1883`);
}

bootstrap();
