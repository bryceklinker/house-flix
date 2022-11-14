import {
  OMDBErrorResponse,
  OMDBItemResponse,
  OMDBSearchItem,
  OMDBType,
} from '../lib';
import { ModelFactory } from '@house-flix/testing';
import { faker } from '@faker-js/faker';
import { format } from 'date-fns';

function createSearchItem(model: Partial<OMDBSearchItem> = {}): OMDBSearchItem {
  const year = faker.datatype.number({
    min: 1970,
    max: new Date().getFullYear(),
  });
  return {
    Title: faker.music.songName(),
    imdbID: faker.datatype.uuid(),
    Poster: faker.image.imageUrl(),
    Year: `${year}`,
    Type: faker.helpers.arrayElement(Object.values(OMDBType)),
    ...model,
  };
}

function createItem(model: Partial<OMDBItemResponse> = {}): OMDBItemResponse {
  const year = faker.datatype.number({
    min: 1970,
    max: new Date().getFullYear(),
  });
  const runtimeInMinutes = faker.datatype.number({ min: 75, max: 240 });
  const releaseDate = faker.date.past();
  return {
    Response: true,
    Actors: ModelFactory.many(() => faker.name.fullName(), 5).join(', '),
    Genre: ModelFactory.many(() => faker.music.genre(), 2).join(', '),
    Poster: faker.image.imageUrl(),
    Year: `${year}`,
    Plot: faker.lorem.sentences(3),
    Director: ModelFactory.many(() => faker.name.fullName(), 2).join(', '),
    Rated: faker.hacker.abbreviation(),
    Runtime: `${runtimeInMinutes} min`,
    Language: 'English',
    Released: format(releaseDate, 'dd MM yyyy'),
    Title: faker.music.songName(),
    ...model,
  };
}

function createErrorItem(
  model: Partial<OMDBErrorResponse> = {}
): OMDBErrorResponse {
  return {
    Response: false,
    Error: faker.lorem.sentence(),
    ...model,
  };
}

export const OMDBModelFactory = {
  ...ModelFactory,
  createSearchItem,
  createItem,
  createErrorItem,
};
