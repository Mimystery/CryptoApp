import { Layout, Typography } from 'antd';

const contentStyle = {
  textAlign: 'center',
  height: 'calc(100vh - 60px)',
  color: '#fff',
  backgroundColor: '#f2f5ff',
  padding: '1rem',
};

export default function WalletWidget(){
    return (
        <Layout.Content>
          <Typography.Title level={3} style={{textAlign: 'left'}}>Portfolio:</Typography.Title>
        </Layout.Content>
    )
}