import { Injectable } from '@nestjs/common';
import { DirectoryChangeEvent } from '../app/file-changes/transport/directory-monitor';
import { EventPattern } from '@nestjs/microservices';

@Injectable()
export class FakeSubscriber {
  private static events: DirectoryChangeEvent[] = [];

  public static getEvents(): DirectoryChangeEvent[] {
    return FakeSubscriber.events;
  }

  @EventPattern<Partial<DirectoryChangeEvent>>({ type: 'add' })
  public handleAdds(event: DirectoryChangeEvent) {
    FakeSubscriber.events = [...FakeSubscriber.events, event];
  }

  public static clearEvents() {
    FakeSubscriber.events = [];
  }
}
