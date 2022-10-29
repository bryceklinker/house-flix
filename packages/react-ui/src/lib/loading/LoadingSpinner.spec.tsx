import { render, screen } from '../../testing';
import { LoadingSpinner } from './LoadingSpinner';

test('when size is missing then height and width is default', () => {
  render(<LoadingSpinner />);

  expect(screen.getByLabelText('spinner')).toHaveClass('h-12');
  expect(screen.getByLabelText('spinner')).toHaveClass('w-12');
});

test('when size is large then height and width is adjusted', () => {
  render(<LoadingSpinner size={'large'} />);

  expect(screen.getByLabelText('spinner')).toHaveClass('h-24');
  expect(screen.getByLabelText('spinner')).toHaveClass('w-24');
});

test('when size is medium then height and width is adjusted', () => {
  render(<LoadingSpinner size={'medium'} />);

  expect(screen.getByLabelText('spinner')).toHaveClass('h-12');
  expect(screen.getByLabelText('spinner')).toHaveClass('w-12');
});

test('when size is small then height and width is adjusted', () => {
  render(<LoadingSpinner size={'small'} />);

  expect(screen.getByLabelText('spinner')).toHaveClass('h-4');
  expect(screen.getByLabelText('spinner')).toHaveClass('w-4');
});
