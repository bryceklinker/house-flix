import { FC, Fragment, useCallback, useState } from 'react';
import { Transition, Dialog } from '@headlessui/react';
import { ColumnFlex, Flex, RowFlex } from '@house-flix/react-ui';
import { HouseFlixHeader } from './HouseFlixHeader';
import { LinkButton } from '../../../../react-ui/src/lib/buttons/LinkButton';

export const HouseFlixShell: FC = () => {
  const [isMenuOpen, setIsMenuOpen] = useState(false);
  const handleOpenMenu = useCallback(
    () => setIsMenuOpen(true),
    [setIsMenuOpen]
  );
  const handleCloseMenu = useCallback(
    () => setIsMenuOpen(false),
    [setIsMenuOpen]
  );
  return (
    <ColumnFlex className={'flex-1'}>
      <HouseFlixHeader onMenuClick={handleOpenMenu} />
      <HouseFlixSideBar open={isMenuOpen} onClose={handleCloseMenu} />
    </ColumnFlex>
  );
};

export type HouseFlixSideBarProps = {
  open?: boolean;
  onClose: () => void;
};

export const HouseFlixSideBar: FC<HouseFlixSideBarProps> = ({
  open,
  onClose,
}) => {
  return (
    <Transition.Root show={open} as={Fragment}>
      <Dialog as={'div'} onClose={onClose} className={'relative z-10'}>
        <Transition.Child
          as={Fragment}
          enter={'ease-in-out duration-500'}
          enterFrom={'opacity-0'}
          enterTo={'opacity-100'}
          leave={'ease-in-out duration-500'}
          leaveFrom={'opacity-100'}
          leaveTo={'opacity-0'}
        >
          <div
            className={
              'fixed inset-0 bg-gray-500 bg-opacity-75 transition-opacity'
            }
          />
        </Transition.Child>

        <div className={'fixed inset-0 overflow-hidden'}>
          <div className={'absolute inset-0 overflow-hidden'}>
            <Flex
              className={
                'pointer-events-none fixed inset-y-0 left-0 max-w-full'
              }
            >
              <Transition.Child
                as={Fragment}
                enter={'transform transition ease-in-out duration-500'}
                enterFrom={'translate-x-0'}
                enterTo={'translate-x-full'}
                leave={'transform transition ease-in-out duration-500'}
                leaveFrom={'translate-x-full'}
                leaveTo={'translate-x-0'}
              >
                <Dialog.Panel
                  className={'pointer-events-auto relative w-screen max-w-md'}
                >
                  <ColumnFlex className={'h-full overflow-y-auto shadow-xl'}>
                    <RowFlex className={'flex-initial'}></RowFlex>

                    <ColumnFlex>
                      <LinkButton to={'/'}>Home</LinkButton>
                    </ColumnFlex>
                  </ColumnFlex>
                </Dialog.Panel>
              </Transition.Child>
            </Flex>
          </div>
        </div>
      </Dialog>
    </Transition.Root>
  );
};
