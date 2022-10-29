import { FC, Fragment } from 'react';
import { Dialog, Transition } from '@headlessui/react';
import { ColumnFlex, Flex, RowFlex, LinkButton } from '@house-flix/react-ui';

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
                enterFrom={'-translate-x-full'}
                enterTo={'translate-x-0'}
                leave={'transform transition ease-in-out duration-500'}
                leaveFrom={'translate-x-0'}
                leaveTo={'-translate-x-full'}
              >
                <Dialog.Panel
                  className={'pointer-events-auto relative w-screen max-w-sm'}
                >
                  <ColumnFlex
                    className={
                      'h-full overflow-y-auto shadow-xl bg-slate-900 px-1 py-4'
                    }
                  >
                    <ColumnFlex>
                      <LinkButton onClick={onClose} to={'/'}>
                        Home
                      </LinkButton>
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
