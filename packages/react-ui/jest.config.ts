/* eslint-disable */
export default {
  displayName: 'react-ui',
  preset: '../../jest.preset.js',
  transform: {
    '^.+\\.[tj]sx?$': 'babel-jest',
  },
  moduleFileExtensions: ['ts', 'tsx', 'js', 'jsx'],
  setupFilesAfterEnv: ['<rootDir>/src/testing/setup.ts'],
  coverageDirectory: '../../coverage/packages/react-ui',
};
