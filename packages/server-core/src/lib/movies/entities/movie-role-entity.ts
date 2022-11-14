import { EntityBase } from '../../common/entities/entity-base';
import { Column, Entity, ManyToOne } from 'typeorm';
import { MovieEntity } from './movie-entity';
import { PersonEntity } from './person-entity';
import { MovieRole } from './movie-role';

@Entity()
export class MovieRoleEntity extends EntityBase {
  @Column('enum')
  role: MovieRole;

  @ManyToOne(() => MovieEntity, (movie) => movie.roles)
  movie: MovieEntity;

  @ManyToOne(() => PersonEntity, (person) => person.roles)
  person: PersonEntity;

  static create(
    role: MovieRole,
    person: PersonEntity,
    movie: MovieEntity
  ): MovieRoleEntity {
    const movieRole = new MovieRoleEntity();
    movieRole.role = role;
    movieRole.movie = movie;
    movieRole.person = person;
    movie.addRole(movieRole);
    person.addRole(movieRole);
    return movieRole;
  }
}
