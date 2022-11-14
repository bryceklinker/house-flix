import { Column, Entity, OneToMany } from 'typeorm';
import { EntityBase } from '../../common/entities/entity-base';
import { MovieRoleEntity } from './movie-role-entity';

@Entity()
export class PersonEntity extends EntityBase {
  @Column('string')
  firstName: string;

  @Column('string', { nullable: true })
  middleName: string;

  @Column('string')
  lastName: string;

  @OneToMany(() => MovieRoleEntity, (role) => role.person)
  roles: MovieRoleEntity[] = [];

  addRole(role: MovieRoleEntity) {
    this.roles = [...this.roles, role];
  }
}
