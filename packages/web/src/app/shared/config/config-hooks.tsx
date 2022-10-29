import {
  createContext,
  FC,
  PropsWithChildren,
  useCallback,
  useContext,
  useEffect,
  useReducer,
} from 'react';
import { LoadingOverlay } from '@house-flix/react-ui';
import { ConfigModel, ConfigModelSchema } from './config-model';

export type ConfigContextState = {
  config: ConfigModel | null;
  isLoading: boolean;
  error: unknown;
  refresh: (() => void) | null;
};
const INITIAL_CONFIG_STATE: Omit<ConfigContextState, 'refresh'> = {
  config: null,
  isLoading: false,
  error: null,
};
export const ConfigContext = createContext<ConfigContextState>({
  ...INITIAL_CONFIG_STATE,
  refresh: () => null,
});

function useConfigContext(): ConfigContextState {
  const context = useContext(ConfigContext);
  if (!context) {
    throw new Error(
      `${useConfigContext.name} must be used in a child of ${ConfigProvider.name}.`
    );
  }

  return context;
}

export function useConfigModel(): ConfigModel | null {
  const { config } = useConfigContext();
  return config;
}

type LoadConfigRequestAction = { type: '[Config] Load Request' };
type LoadConfigSuccessAction = {
  type: '[Config] Load Success';
  payload: ConfigModel;
};
type LoadConfigFailedAction = { type: '[Config] Load Failed' };
type ConfigAction =
  | LoadConfigFailedAction
  | LoadConfigSuccessAction
  | LoadConfigRequestAction;

function configReducer(
  state: ConfigContextState,
  action: ConfigAction
): ConfigContextState {
  switch (action.type) {
    case '[Config] Load Request':
      return { ...state, isLoading: true };
    case '[Config] Load Success':
      return { ...state, isLoading: false, config: action.payload };
    case '[Config] Load Failed':
      return { ...state, error: 'Failed to load config.', isLoading: false };
  }
}

export type ConfigProviderProps = PropsWithChildren & {
  configUrl: string;
};

export const ConfigProvider: FC<ConfigProviderProps> = ({
  children,
  configUrl,
}) => {
  const [state, dispatch] = useReducer(configReducer, {
    ...INITIAL_CONFIG_STATE,
    refresh: () => null,
  });
  const refresh = useCallback(async () => {
    dispatch({ type: '[Config] Load Request' });
    try {
      const response = await fetch(configUrl);
      const config = await response.json();
      dispatch({
        type: '[Config] Load Success',
        payload: await ConfigModelSchema.parseAsync(config),
      });
    } catch (err) {
      console.error(err);
      dispatch({ type: '[Config] Load Failed' });
    }
  }, [dispatch, configUrl]);
  useEffect(() => {
    if (state.isLoading || state.config) {
      return;
    }
    refresh().catch((err) => console.error(err));
  }, [refresh, state]);

  return (
    <ConfigContext.Provider value={{ ...state, refresh }}>
      <>
        {state.isLoading && (
          <LoadingOverlay>Loading Configuration...</LoadingOverlay>
        )}
        {children}
      </>
    </ConfigContext.Provider>
  );
};
