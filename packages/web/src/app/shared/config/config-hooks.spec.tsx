import { ConfigModel } from './config-model';
import {
  render,
  TestingApi,
  screen,
  waitForElementToBeRemoved,
} from '../../../testing';
import { ConfigProvider, useConfigModel } from './config-hooks';

test('when rendered then config is available', async () => {
  TestingApi.setupGet<ConfigModel>('http://localhost/config', {
    api: { url: 'https://google.com' },
  });

  render(
    <ConfigProvider configUrl={'http://localhost/config'}>
      <TestingComponent />
    </ConfigProvider>
  );

  await waitForElementToBeRemoved(() => screen.queryByRole('status'));
  expect(screen.getByLabelText('config')).toHaveTextContent(
    'https://google.com'
  );
});

function TestingComponent() {
  const config = useConfigModel();
  return <div aria-label={'config'}>{JSON.stringify(config)}</div>;
}
