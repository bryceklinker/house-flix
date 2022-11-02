/* eslint-disable */
export default {
  displayName: 'react-ui',
  preset: '../../jest.preset.js',
  transform: {
    '^.+\\.[tj]sx?$': [
      '@swc/jest',
      { jsc: { transform: { react: { runtime: 'automatic' } } } },
    ],
  },
  moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx'],
  setupFilesAfterEnv: ['<rootDir>/src/testing/setup.ts'],
  coverageDirectory: '../../coverage/packages/react-ui',
};
