import { CustomTransportStrategy, Server } from '@nestjs/microservices';

export class FileChangeTransportStrategy
  extends Server
  implements CustomTransportStrategy
{
  listen(callback: (...optionalParams: unknown[]) => unknown) {
    this.messageHandlers.forEach((handler, pattern) => {});
  }

  close() {}
}
