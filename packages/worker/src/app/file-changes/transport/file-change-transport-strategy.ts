import { CustomTransportStrategy, Server } from '@nestjs/microservices';
import { DirectoryMonitor, DirectorySubscription } from './directory-monitor';

export type FileChangeTransportStrategyOptions = {
  directory: string;
};

export class FileChangeTransportStrategy
  extends Server
  implements CustomTransportStrategy
{
  private readonly monitor: DirectoryMonitor;
  private subscriptions: DirectorySubscription[];

  constructor(private readonly options: FileChangeTransportStrategyOptions) {
    super();

    this.monitor = new DirectoryMonitor(options.directory);
  }

  async listen(callback: (...optionalParams: unknown[]) => unknown) {
    const promises = [...this.messageHandlers.values()].map((handler) =>
      this.monitor.subscribe(handler)
    );
    this.subscriptions = await Promise.all(promises);
    await this.monitor.start();
    callback();
  }

  async close() {
    this.subscriptions.forEach((s) => s.unsubscribe());
    await this.monitor.close();
  }
}
