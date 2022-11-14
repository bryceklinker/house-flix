import { Column, PrimaryGeneratedColumn } from 'typeorm';

export abstract class EntityBase {
  @PrimaryGeneratedColumn('uuid')
  id: string;
}
