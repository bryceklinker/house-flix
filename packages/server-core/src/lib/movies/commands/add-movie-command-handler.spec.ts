import { createServerCoreTestingModule } from '../../../testing';
import { AddMovieCommand } from './add-movie-command-handler';

describe('AddMovieCommandHandler', () => {
  test('when movie is added then adds movie to database', async () => {
    const { commandBus } = await createServerCoreTestingModule();

    await commandBus.execute(new AddMovieCommand(''));
  });
});
