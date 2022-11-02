import { transports, format } from 'winston';
import { WinstonModule } from 'nest-winston';

type HouseFlixTransformableInfo = Record<string, string> & {
  level: string;
  message: string;
  appName: string;
};

const houseFlixFormat = format.printf(
  ({ level, appName, message, ...rest }: HouseFlixTransformableInfo) => {
    const msg = `[${appName}] ${level}: ${message}`;
    return `${msg} (${JSON.stringify(rest)})`;
  }
);

export type CreateHouseFlixLoggerProps = Record<string, string> & {
  appName: string;
  level?: 'debug' | 'info' | 'warn' | 'error';
};
export function createHouseFlixLogger(props: CreateHouseFlixLoggerProps) {
  return WinstonModule.createLogger({
    defaultMeta: props,
    format: format.combine(
      format.colorize(),
      format.ms(),
      format.splat(),
      format.timestamp(),
      houseFlixFormat
    ),
    level: props.level || 'info',
    transports: new transports.Console(),
  });
}
