-- =========================
-- Categories
-- =========================

insert into categories (id, name, description, is_active, created_at, updated_at)
values
  ('11111111-1111-1111-1111-111111111111', 'Food',        'Food related products',        true, now(), now()),
  ('22222222-2222-2222-2222-222222222222', 'Books',       'Books related products',       true, now(), now()),
  ('33333333-3333-3333-3333-333333333333', 'Electronics', 'Electronics related products', true, now(), now()),
  ('44444444-4444-4444-4444-444444444444', 'Clothing',    'Clothing related products',    true, now(), now()),
  ('55555555-5555-5555-5555-555555555555', 'Home',        'Home related products',        true, now(), now());

-- =========================
-- Products
-- =========================
-- 25 products per category
-- ~80% active
-- ~20% inactive
-- ~20% out of stock (stock_quantity = 0)

insert into products (
    id,
    category_id,
    name,
    price,
    stock_quantity,
    is_active,
    created_at,
    updated_at
)
select
    gen_random_uuid(),
    c.id,
    c.name || ' Product ' || g.n,
    round((random() * 900 + 10)::numeric, 2),        -- price: 10.00 - 910.00
    case
        when random() < 0.2 then 0                  -- 20% out of stock
        else (random() * 100)::int + 1
    end as stock_quantity,
    random() < 0.8,                                 -- 80% active
    now(),
    now()
from categories c
cross join generate_series(1, 25) as g(n);
