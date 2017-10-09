using System;
using System.Collections.Generic;
using System.Text;

namespace aehyok.AliPay
{
    public class Config
    {
        // 应用ID,您的APPID
        public static string AppId = "2016080800191598";

        // 支付宝网关
        public static string Gatewayurl = "https://openapi.alipaydev.com/gateway.do";

        // 商户私钥，您的原始格式RSA私钥
        public static string PrivateKey = "MIIEpQIBAAKCAQEA4VZCECpfF55c//Q4/uHHAkQR0oqX+KsxCsCUiZTZqJzrwwKMPO/USSJ0mAUvnVTyoqCHh47x7yV5M0XZ29311J4Dk0SzjWwaqsjsNLV8MfeCFLBTl7Bn3RPK3dDBlTyzNZ7LBNIXBwzfmAlKgPfdDl6J8p4Kjki9+GpnK1lCN8rrI/jV4viqct4psS2WB5GEudOo8W1RbSgvu5V/iZbOpv/6vfaiwsEDnAVxJW+6yJO5ujh92mej5DZrpZp7yKOmYS8eDG9vZebyF3bWKHAbi3xNHq89Zztf6X+SUYt7VYEJWbvuNdcEf1+hMR6HrZv58jhLkV3ruuYx3kJe9vLC/wIDAQABAoIBAD72Yy8xJ7DpxwKdsu9FFt5DNtelciFBdEqU1Ow8czTx485xf83nFWH6BqoTiOJlQga+5B+0iVVRGzSAaAdPVck6/bTAlYsGkcz2p0lzDGjJx8cQHctctyjGxwCDvBN/CnQJEqEeMuD9b9yDfBXdLB8/uzvXfQuUt8AZuwoZvq7ntdxHTzIfSdL8jk4EjJOimAW651RixJ/5NaSeV5A22I0dpxvbBb4ENzYM7izhhDlKH0DrYpq1VSET3XSE1TXbJ6Ok0+n17u8EuqQWlWxvXl3qBOuYvUIxQjSaaX0qFf0JA1yEB6t1lqz+rvmKHFFTaBb2DnED6Li1SQzuxL1g2YECgYEA9SZACzy+1F12HtqpEGC4oWaGs93Q9Rhv3nogMJZXTcjOYlB+kY2c1Wcta31sOziZJeLZO3swYyS3RBpPb5llL1YhyttZ5EGr0rtb3McdpR0PnrV074qEgUfCrzTlldjy2A2E70jLyhBVSTHUI3qosO1+qH0oo1ERTBsM7IwBOMcCgYEA60+EERaIoLRaRJ9z7wBxKXGuEyvZvwvxY4QPB2Ss3xfQ6EJ+Mi+FoZfDfxaggVAHp7TG4o1oBGCIAqG9qJwrmWUFKrkOCayfpIyDvxHZadTdJuUtp4QojHW4cLTPriNu+9+D/pj6pW2C9nhltcsq2XtM9zHsrMfN8FszaRO+HAkCgYEA19ld+ob53zKLlo8g3PLMnhAP+r8MBUIG/fS58w3swOhWD+qgBriRprS6ITLF4T7sWZdrfhvxdtoVIzmnR7Pgi9Vi26JXe8r25w1gzPvk9kSoRC2xUFi+YrY0jOAeZdonuGPu8GFATiIjddcR2ktdzKrQIrpQ1bvDZydKMdz0FnECgYEAkv+opDgsgCBK5+WouhzPlLFkqqtRKu0eDioYbWjyKagqJH4tcdKIK4uwYDtVZ5MERTtEp5Do8xsOvH4oCiYNgpU+JhNW9qo1TMlPIFTZOij1QvAWljiUUDLWVupNdx7BnW1jevA8XI3OK4TJHUlfal+BlEdG8CUdvNJsim7/62kCgYEA0n7NndTvua3fZCbYhZDQ6xObWAWBErIB3jNgga7AgK+Dpy+GYtnS1WsXGBHO1LnOtzpgz+b2/KGu6ZqtByWvrhAXs09ymc/iMB37B1MxqTb7/YCFNmV/lU5VPDAf1NJP05OqPNx1cAJFAsSZ4bBtDV7ccOv6cpP9ynsleu6Zf7s=";

        // 支付宝公钥,查看地址：https://openhome.alipay.com/platform/keyManage.htm 对应APPID下的支付宝公钥。
        public static string AlipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAmT/USe7HCekeEwOADqAp/BEMiPIivzOuYu36SLX6WsXcLyHMz7MKP6MkX/Jci530SKZVCSPZYD92ZyATmtu44gCpMTLV3ZVLwQTE9BsR4qVk5eJDHCWu5cNcKRlZVFQM7UFLmK1lVcw1h9yIjVQW1dlnPhqQEVA3MjEKVYU4gk3bVj+WeA9uuUUzOXaFHsSg647MC/PID7FBN8pcQSEtjjdzkwI0tyIX6iSCmTXx72xDibBVuw8F8bmHPAVmy1VT4xJ6n6VPfxWIFSp8JMjhj2vhOgrD5TjozonPUHhuuCwEzWB1MZUcupjtJgSFmhQf3JnMiJjinLWqpjEargTWQwIDAQAB";

        // 签名方式
        public static string SignType = "RSA2";

        // 编码格式
        public static string CharSet = "UTF-8";
    }
}
