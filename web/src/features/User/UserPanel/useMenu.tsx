import {  Icon } from '@lobehub/ui';
import {
  Book,
  Feather,
  LifeBuoy,
  LogOut,
  Mail,
  Settings2,
} from 'lucide-react';
import { Link } from 'react-router-dom';
import type { MenuProps } from '@/components/Menu';
import { DOCUMENTS, EMAIL_SUPPORT, GITHUB_ISSUES, mailTo } from '@/const/url';
import { useQueryRoute } from '@/hooks/useQueryRoute';
import { useUserStore } from '@/store/user';
import { authSelectors } from '@/store/user/selectors';

export const useMenu = () => {
  const router = useQueryRoute();
  const [isLogin, isLoginWithAuth] = useUserStore((s) => [
    authSelectors.isLogin(s),
    authSelectors.isLoginWithAuth(s)
  ]);

  const helps: MenuProps['items'] = [
    {
      children: [
        {
          icon: <Icon icon={Book} />,
          key: 'docs',
          label: (
            <Link to={DOCUMENTS} target={'_blank'}>
                文档
            </Link>
          ),
        },
        {
          icon: <Icon icon={Feather} />,
          key: 'feedback',
          label: (
            <Link to={GITHUB_ISSUES} target={'_blank'}>
                反馈
            </Link>
          ),
        },
        {
          icon: <Icon icon={Mail} />,
          key: 'email',
          label: (
            <Link to={mailTo(EMAIL_SUPPORT)} target={'_blank'}>
                邮件支持
            </Link>
          ),
        },
      ],
      icon: <Icon icon={LifeBuoy} />,
      key: 'help',
      label: '帮助',
    },
    {
      type: 'divider',
    },
  ];

  const settings: MenuProps['items'] = [
    {
      icon: <Icon icon={Settings2} />,
      key: 'settings',
      label: '设置',
      onClick: () =>{

      },
    },
  ]

  const mainItems = [
    {
      type: 'divider',
    },
    ...(isLogin ? settings : []),
    ...helps,
  ].filter(Boolean) as MenuProps['items'];

  const logoutItems: MenuProps['items'] = isLoginWithAuth
    ? [
        {
          icon: <Icon icon={LogOut} />,
          key: 'logout',
          label: <span>退出登录</span>,
        },
      ]
    : [];

  return { logoutItems, mainItems };
};
