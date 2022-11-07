import { Stats } from 'fs';
import { FSWatcher, watch } from 'chokidar';
import { v4 as uuid } from 'uuid';

export type DirectoryChangeEventType = 'add' | 'change' | 'delete';
export type DirectoryChangeEvent = {
  path: string;
  isFile: boolean;
  isDirectory: boolean;
  type: DirectoryChangeEventType;
};

export type DirectoryChangeEventHandler = (
  change: DirectoryChangeEvent
) => Promise<unknown> | unknown;
export type DirectorySubscriber = {
  id: string;
  handler: DirectoryChangeEventHandler;
};

export type DirectorySubscription = {
  unsubscribe: () => Promise<unknown> | unknown;
};

export class DirectoryMonitor {
  private watcher: FSWatcher;
  private subscribers: DirectorySubscriber[];

  constructor(private readonly directory: string) {
    this.subscribers = [];
  }

  start(): Promise<void> {
    this.watcher = watch(this.directory, {
      alwaysStat: true,
      awaitWriteFinish: true,
    });
    this.watcher.on('add', (...args) => this.broadcastChange('add', ...args));
    this.watcher.on('change', (...args) =>
      this.broadcastChange('change', ...args)
    );
    this.watcher.on('unlink', (...args) =>
      this.broadcastChange('delete', ...args)
    );
    this.watcher.on('addDir', (...args) =>
      this.broadcastChange('add', ...args)
    );
    this.watcher.on('unlinkDir', (...args) =>
      this.broadcastChange('delete', ...args)
    );
    return Promise.resolve();
  }

  subscribe(
    handler: DirectoryChangeEventHandler
  ): Promise<DirectorySubscription> {
    const subscriber: DirectorySubscriber = {
      id: uuid(),
      handler,
    };
    this.subscribers = [...this.subscribers, subscriber];
    return Promise.resolve({
      unsubscribe: () => this.removeSubscriber(subscriber),
    });
  }

  async close(): Promise<void> {
    if (this.watcher) {
      await this.watcher.close();
    }
  }

  private async broadcastChange(
    type: DirectoryChangeEventType,
    path: string,
    stats?: Stats
  ) {
    await this.broadcast({
      path,
      type,
      isDirectory: stats ? stats.isDirectory() : false,
      isFile: stats ? stats.isFile() : false,
    });
  }

  private removeSubscriber(subscriber: DirectorySubscriber) {
    this.subscribers = this.subscribers.filter((s) => s.id !== subscriber.id);
  }

  private async broadcast(event: DirectoryChangeEvent) {
    await Promise.all(this.subscribers.map((s) => s.handler(event)));
  }
}
