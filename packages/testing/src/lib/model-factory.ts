function many<T>(factory: () => T, count: number): T[] {
  return Array(count).fill(null).map(factory);
}

export const ModelFactory = {
  many,
};
