import { Layout, Typography } from 'antd';
import { useContext } from 'react';
import { CryptoContext } from '../../context/crypto-context';

const contentStyle = {
  textAlign: 'center',
  height: 'calc(100vh - 60px)',
  color: '#fff',
  backgroundColor: '#f2f5ff',
  padding: '1rem',
};

export default function WalletWidget(){
const {wallet, prices} = useContext(CryptoContext)

console.log(wallet)
    return (
        <Layout.Content>
          <Typography.Title level={3} style={{textAlign: 'left'}}>
            Portfolio: {}
          </Typography.Title>
        </Layout.Content>
    )
}