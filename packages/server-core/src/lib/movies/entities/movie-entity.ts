import { EntityBase } from '../../common/entities/entity-base';
import { Column, Entity, OneToMany } from 'typeorm';
import { MovieRoleEntity } from './movie-role-entity';
import { MovieRole } from './movie-role';
import { PersonEntity } from './person-entity';

@Entity()
export class MovieEntity extends EntityBase {
  @Column('string')
  title: string;

  @Column('string')
  description: string;

  @Column('date')
  releaseDate: Date;

  @OneToMany(() => MovieRoleEntity, (role) => role.movie)
  roles: MovieRoleEntity[] = [];

  @Column('string')
  filePath: string;

  addPerson(role: MovieRole, person: PersonEntity) {
    const movieRole = MovieRoleEntity.create(role, person, this);
    this.roles = [...this.roles, movieRole];
  }

  addRole(role: MovieRoleEntity) {
    this.roles = [...this.roles, role];
  }
}
