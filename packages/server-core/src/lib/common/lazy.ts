export class Lazy<T> {
  private _value: T | null;

  get value(): T {
    return this._value || (this._value = this.factory());
  }

  constructor(private readonly factory: () => T) {}
}
