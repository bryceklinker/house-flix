import { MovieEntity } from './movie-entity';
import { MovieRole } from './movie-role';
import { PersonEntity } from './person-entity';

describe('MovieEntity', () => {
  test('when role is added then movie has new role', () => {
    const person = new PersonEntity();
    const movie = new MovieEntity();
    movie.addPerson(MovieRole.Actor, person);

    expect(movie.roles).toContainEqual({
      movie: movie,
      person: person,
      role: MovieRole.Actor,
    });
    expect(person.roles).toContainEqual({
      movie: movie,
      person: person,
      role: MovieRole.Actor,
    });
  });
});
